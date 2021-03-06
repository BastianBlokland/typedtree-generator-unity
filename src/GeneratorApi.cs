﻿using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

using TypedTree.Generator.Core.Mapping;
using TypedTree.Generator.Core.Mapping.NodeComments;
using TypedTree.Generator.Core.Serialization;
using TypedTree.Generator.Core.Utilities;

namespace TypedTree.Generator.Editor
{
    /// <summary>
    /// Public api into the typedtree-generator.
    /// </summary>
    public static class GeneratorApi
    {
        /// <summary>
        /// Generate treescheme json file for a given type.
        /// </summary>
        /// <param name="options">Configuration options</param>
        /// <param name="logContext">Unity object to use as context for logs</param>
        public static void GenerateSchemeToFile(Options options, UnityEngine.Object logContext)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            // Create logger.
            var logger = new UnityLogger(options.VerboseLogging, logContext);
            if (string.IsNullOrEmpty(options.RootAliasTypeName))
            {
                logger.LogCritical($"Failed to generate: No '{nameof(Options.RootAliasTypeName)}' provided");
                return;
            }

            // Add manually entered comments.
            INodeCommentProvider nodeCommentProvider = null;
            if (options.Comments != null)
            {
                try
                {
                    nodeCommentProvider = new ManualNodeCommentProvider(
                        options.Comments.ToDictionary(nc => nc.NodeType, nc => nc.Comment));
                }
                catch
                {
                    logger.LogWarning("Failed to add comments, does it contain a duplicate type?");
                }
            }

            // Generate.
            GenerateSchemeToFile(
                options.RootAliasTypeName,
                options.FieldSource,
                options.OutputPath,
                options.TypeIgnorePattern,
                nodeCommentProvider,
                logger);
        }

        /// <summary>
        /// Generate treescheme json file for a given type.
        /// </summary>
        /// <param name="rootAliasTypeName">Fullname of the type to use as the root of the tree</param>
        /// <param name="fieldSource">Enum to indicator how to find fields on types</param>
        /// <param name="outputPath">Path to save the output file relative to the Assets directory</param>
        /// <param name="typeIgnorePattern">Optional regex pattern to ignore types</param>
        /// <param name="nodeCommentProvider">Optional provider of node-comments</param>
        /// <param name="logger">Optional logger for diagnostic output</param>
        public static void GenerateSchemeToFile(
            string rootAliasTypeName,
            FieldSource fieldSource,
            string outputPath,
            Regex typeIgnorePattern = null,
            INodeCommentProvider nodeCommentProvider = null,
            Core.ILogger logger = null)
        {
            // Generate the json.
            var json = GenerateScheme(
                rootAliasTypeName, fieldSource, typeIgnorePattern, nodeCommentProvider, logger);
            if (json != null)
            {
                // Write the file.
                try
                {
                    var fullPath = Path.Combine(UnityEngine.Application.dataPath, outputPath);
                    var outputDir = Path.GetDirectoryName(fullPath);
                    if (!Directory.Exists(outputDir))
                    {
                        logger?.LogDebug($"Creating output directory: '{outputDir}'");
                        Directory.CreateDirectory(outputDir);
                    }

                    File.WriteAllText(fullPath, json);
                    logger?.LogInformation($"Saved scheme: '{outputPath}'");
                }
                catch (Exception e)
                {
                    logger?.LogCritical($"Failed to save file: {e.Message.ToDistinctLines()}");
                }
            }
        }

        /// <summary>
        /// Generate treescheme json for a given type.
        /// </summary>
        /// <param name="rootAliasTypeName">Fullname of the type to use as the root of the tree</param>
        /// <param name="fieldSource">Enum to indicator how to find fields on types</param>
        /// <param name="typeIgnorePattern">Optional regex pattern to ignore types</param>
        /// <param name="nodeCommentProvider">Optional provider of node-comments</param>
        /// <param name="logger">Optional logger for diagnostic output</param>
        /// <returns>Json string representing the scheme</returns>
        public static string GenerateScheme(
            string rootAliasTypeName,
            FieldSource fieldSource,
            Regex typeIgnorePattern = null,
            INodeCommentProvider nodeCommentProvider = null,
            Core.ILogger logger = null)
        {
            try
            {
                // Gather all the types.
                var typeCollection = TypeCollection.Create(AppDomain.CurrentDomain.GetAssemblies(), logger);

                // Create mapping context.
                var context = Context.Create(
                    typeCollection,
                    fieldSource,
                    typeIgnorePattern,
                    nodeCommentProvider,
                    logger);

                // Map the tree.
                var tree = TreeMapper.MapTree(context, rootAliasTypeName);

                // Serialize the scheme.
                return JsonSerializer.ToJson(tree, JsonSerializer.Mode.Pretty);
            }
            catch (Exception e)
            {
                logger?.LogCritical($"Failed to generate scheme: {e.Message.ToDistinctLines()}");
                return null;
            }
        }
    }
}
